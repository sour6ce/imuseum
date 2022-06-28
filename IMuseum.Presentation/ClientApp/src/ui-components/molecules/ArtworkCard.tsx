import { Badge } from "../atoms/Badge";
import { Button } from "../atoms/Button";
import { selectStatusColor } from "../../utils/selectStatusColor";
import Icon from "../atoms/Icon";
import { useNavigate } from "react-router-dom";
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
  };
}

const ArtworkCard: React.FC<ArtworkCardProps> = (props) => {
  const nav = useNavigate()
  const openInGallery = () => {nav('/gallery/'+props.artwork.id)};

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
          <Button color="primary" onClick={openInGallery}>
            <div className="flex items-center gap-1">
              Gallery
              {<Icon family="fontawesome" name="image"/>}
            </div>
          </Button>
        </div>
      </div>
    </div>
  );
};

export default ArtworkCard; 