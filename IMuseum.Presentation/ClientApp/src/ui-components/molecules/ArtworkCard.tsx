import { Badge } from "../atoms/Badge";
import { Button } from "../atoms/Button";
import { selectStatusColor } from "../../utils/selectStatusColor";
import Icon from "../atoms/Icon";
interface ArtworkCardProps {
  artwork: {
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
  const editArtwork = () => {};
  const openInGallery = () => {};


  return (
    <div className="flex gap-5 bg-gray-600 p-6 rounded-xl">
      <div className="w-60 bg-slate-400 rounded-md flex flex-shrink-0">
        <img src={props.artwork.link} alt={props.artwork.name} />
      </div>
      <div>

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
        
        <div className="flex h-full items-end pb-5 gap-3 text-sm flex-shrink-0">
          <Button color="success" onClick={editArtwork}>
            <div className="flex items-center gap-1">
              Edit
              {<Icon family="fontawesome" name="gear"/>}
            </div>
            
          </Button>
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