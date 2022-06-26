import { Icon } from '../atoms/Icon';
import Link from '../atoms/Link';
import { BreadcrumbItem, BreadcrumbItemProps } from '../atoms/BreadcrumbItem';

export interface BreadcrumbProps {
  items: BreadcrumbItemProps[];
}

export const Breadcrumb = (props: BreadcrumbProps) => {
  return (
    <>
      <Link href="/" className="text-muted text-hover-primary">
        <Icon name="house" />
      </Link>
      <ul className="breadcrumb breadcrumb-separatorless fw-bold fs-7 my-1">
        {props.items.map((br, index) => (
          <BreadcrumbItem {...br} key={index} />
        ))}
      </ul>
    </>
  );
};
