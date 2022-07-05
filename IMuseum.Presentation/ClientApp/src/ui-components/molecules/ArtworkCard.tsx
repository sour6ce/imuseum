import { Badge } from "../atoms/Badge";
import { Button } from "../atoms/Button";
import { selectStatusColor } from "../../utils/selectStatusColor";
import Icon from "../atoms/Icon";
import { useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { Modal } from "../atoms/Modal";
import Input from "../atoms/Input";
import { StaticService } from "../../services/StaticService";
import {Room} from '../../types/Room'
import { ArtworkService } from "../../services/ArtworkService"
import { mutate } from "swr";
interface ArtworkCardProps {
  artwork: {
    id: number;
    link: string;
    name: string;
    author: string;
    type: string;
    period: string;
    description: string;
    status: string;
    room: string;
  };
  mutate: any;
}

const ArtworkCard: React.FC<ArtworkCardProps> = (props) => {
  const [rooms, setRooms]= useState<Room[]>([])
  useEffect(()=>{
    StaticService.getRooms().then((res)=>{
      setRooms(res)
    })
  },[])
  const [newRoom, setNewRoom] = useState<Room>();
  const nav = useNavigate()
  const openInGallery = () => {nav('/gallery/'+props.artwork.id)};

  const [moveModalShow, setMoveModalShow] = useState(false);

  return (
    <div className="flex gap-5 w-full bg-gray-600 p-6 rounded-xl">
      <div className="w-60 bg-slate-400 rounded-md flex flex-shrink-0">
        <img src={props?.artwork?.link} alt={props.artwork.name} />
      </div>
      <div className="flex-grow-1 w-full">

        <span className="text-2xl font-bold">{props.artwork.name}</span>
        <div className="flex gap-4 text-sm mb-3 uppercase">
          <span>{props.artwork.author}</span>
          <span className="text-primary-light">{props.artwork.type}</span>
          <span className="text-primary-light">{props.artwork.period}</span>
        </div>
        <span className=" text-sm">{props.artwork.description}</span>

      </div>

      <div>
        <div className="flex justify-end">
          <Badge
            color={selectStatusColor(props.artwork.status)}
            className=" text-sm relative   right-0"
          >
            {props.artwork.status}
          </Badge>
        </div>
        
        <div className="flex h-full items-end pb-5 gap-3 text-sm flex-shrink-0 ml-10">
          <Button color="success" onClick={()=>setMoveModalShow(true)}>
            <div className="flex items-center gap-1">
              Move
              {<Icon family="fontawesome" name="arrow-right-arrow-left"/>}
            </div>
          </Button>
          <Button color="primary" onClick={openInGallery}>
            <div className="flex items-center gap-1">
              Gallery
              {<Icon family="fontawesome" name="image"/>}
            </div>
          </Button>
        </div>
        <Modal
          hanldeModalClose={() => setMoveModalShow(false)}
          size='2xl'
          isOpen={moveModalShow}
        >
          <div className="w-full flex flex-col text-gray-100">
            <span className="text-2xl font-bold">{props.artwork.name}</span>
            <div className="flex flex-row justify-between w-full px-5 mt-5 items-center">
              <Badge
                color={selectStatusColor(props.artwork.status)}
                className=" text-sm"
              >
                {props.artwork?.room}
              </Badge>
              <span>
              <Button color="gray-900" onClick={()=>{
                ArtworkService.moveToRoom(`${props.artwork.id}`,newRoom?.id).finally(()=>{
                  setMoveModalShow(false)
                  props?.mutate()
                })
              }}>
                <Icon family="fontawesome" name="arrow-right-arrow-left" className="mr-3"/>
                Move
              </Button></span>
              <span className="flex flex-row">
                <Input
                  name="room" 
                  placeholder="Room"
                  type="select"
                  value={newRoom}
                  onChange={(newV)=>setNewRoom(newV)}
                  options={rooms}
                  getOptionLabel={(o)=>(o as Room).name}
                  getOptionValue={(o)=>`${(o as Room).id}`}
                  containerClassName='self-center mt-3'
                  isClearable
                />
                <Button color="danger" onClick={()=>{
                  ArtworkService.moveToStorage(`${props.artwork.id}`).finally(()=>{
                    setMoveModalShow(false)
                    props?.mutate()
                  })
                }} textColor="gray-100" className="self-center py-2 px-3 rounded-sm ml-5">
                  <Icon family="fontawesome" name="box-archive" className="mr-2"/>
                  To Storage
                </Button>
              </span>
            </div>
          </div>
        </Modal>
      </div>
    </div>
  );
};

export default ArtworkCard; 