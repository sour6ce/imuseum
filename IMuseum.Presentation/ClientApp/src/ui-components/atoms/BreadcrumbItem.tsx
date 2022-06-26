import { Link } from './Link';

export interface BreadcrumbItemProps {
  path: string;
  name: string;
  disable?: boolean;
}
export const BreadcrumbItem = (props: BreadcrumbItemProps) => {
  return (
    <li className="breadcrumb-item">
      <div className="bullet bg-gray-300 w-5px h-2px mx-3" />
      <div className="text-muted">
        {!props.disable ? (
          <Link href={props.path} className="text-muted text-hover-primary">
            {props.name}
          </Link>
        ) : (
          <div>{props.name}</div>
        )}
      </div>
    </li>
  );
};
