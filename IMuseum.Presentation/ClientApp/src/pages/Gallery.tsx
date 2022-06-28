import IconLogo from "../ui-components/atoms/IconLogo";
import { Popover } from "../ui-components/atoms/Popover";
import { NavLink } from "react-router-dom";
import classNames from 'classnames'

interface GalleryProps {
  user?: any;
}
export const homeLinks = [
  {
    label: 'Home',
    to: '/home'
  },
  {
    label: 'Gallery',
    to: '/gallery'
  },
  {
    label: 'Dashboard',
    to: '/dashboard'
  },
]

const Gallery: React.FC<GalleryProps> = (props) => {
  const artworks = [
    {
      link: "fdf",
      title: "dfd",
    },
  ];

  return (
    <div className="absolute inset-0 flex flex-row bg-gray-600 text-gray-100 overflow-hidden">
      <div className="w-full flex flex-col">
        <div className="bg-gray-900 w-full h-min p-3 flex justify-between">
          <div>
          <div className="flex flex-row justify-end gap-24">
        <div className="flex gap-8">
          {homeLinks.map((l)=>(
            <NavLink to={l.to} className={({isActive})=>classNames('text-xl hover:text-primary-lighter ',{
              'font-black': isActive,
              'font-semibold': !isActive
            })}>{l.label}</NavLink>
          ))}
        </div>
      </div>
          </div>
          <div>
            <IconLogo height={50} width={150} />
          </div>

          <div className="flex gap-x-3 flex-row items-center text-lg alig">
            <span className="mr-3">
              Welcome,{" "}
              <span className="font-bold">
                {`${props.user?.first_name ?? "John"} ${
                  props.user?.last_name ?? "Doe"
                }`}{" "}
              </span>
            </span>
            <Popover
              buttonProps={{
                color: "gray-400",
                textColor: "primary-light",
                className: "rounded-md px-4 py-2",
              }}
              render={({ open, close }) => (
                <div className="flex justify-between p-5"></div>
              )}
              position="right"
            >
              <span className="font-extrabold text-xl text-primary-light px-0.5">
                {props.user?.first_name?.[0].toUpperCase() ?? "J"}
              </span>
            </Popover>
          </div>
        </div>

        <div className="p-16">
          {artworks.map((artwork) => (
            <img src={artwork.link} alt={artwork.title} />
          ))}
        </div>
      </div>
    </div>
  );
};

export default Gallery;
