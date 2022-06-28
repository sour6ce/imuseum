import { Icon } from '../atoms/Icon';
import Link from '../atoms/Link';
import { BreadcrumbItem, BreadcrumbItemProps } from '../atoms/BreadcrumbItem';

export interface BreadcrumbProps {
  items: BreadcrumbItemProps[];
}

export const Breadcrumb = (props: BreadcrumbProps) => {
  return (
    <>
      <Link to="/home" className="text-gray-200 hover:text-primary-light self-center">
        <Icon name="house" family='fontawesome' />
      </Link>
      <ul className="list-none flex flex-row gap-2 font-bold text-xl my-1">
        {props.items.map((br, index) => (
          <BreadcrumbItem {...br} key={index} />
        ))}
      </ul>
    </>
  );
};
