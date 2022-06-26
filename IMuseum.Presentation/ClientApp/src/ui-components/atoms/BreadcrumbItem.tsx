import { Link } from './Link';

export interface BreadcrumbItemProps {
  path: string;
  name: string;
  disable?: boolean;
}
export const BreadcrumbItem = (props: BreadcrumbItemProps) => {
  return (
    <li className="list-item">
      <div className="bg-gray-300 w-[5px] h-[2px] mx-3" />
      <div className="text-gray-300">
        {!props.disable ? (
          <Link to={props.path} className="text-gray-300 hover:text-primary">
            {props.name}
          </Link>
        ) : (
          <div>{props.name}</div>
        )}
      </div>
    </li>
  );
};
