import { Link } from './Link';

export interface BreadcrumbItemProps {
  path: string;
  name: string;
  disable?: boolean;
}
export const BreadcrumbItem = (props: BreadcrumbItemProps) => {
  return (
    <li className="flex flex-row">
      <div className="bg-gray-200 w-[5px] h-[2px] mx-3 self-center" />
      <div className="text-gray-300 self-center flex">
        {!props.disable ? (
          <Link to={props.path} className="text-gray-200 hover:text-primary-light text-sm font-semibold">
            {props.name}
          </Link>
        ) : (
          <div>{props.name}</div>
        )}
      </div>
    </li>
  );
};
