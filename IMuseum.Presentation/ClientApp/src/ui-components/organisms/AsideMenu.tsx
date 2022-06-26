import Icon, { IconFamily } from "../atoms/Icon";
import Link from "../atoms/Link"
import classNames from "classnames";
import IconLogo from "../atoms/IconLogo";

interface AsideMenuProps {
  items: {
    icon: string;
    iconFamily: IconFamily;
    to: string;
    name: string;
  }[];
  active: string;
}

export const AsideMenu: React.FC<AsideMenuProps> = (props) => {
  return (
    <div className=" w-[300px] h-full pt-9 pb-9  z-40 left-0 bg-gray-800 transition-all">
      <div className="flex justify-center mb-10">
        <IconLogo width={150} height={68} className='w-full'/>
      </div>
      {props.items.map((item) => (
        <Link to={item.to} key={item.to}>
          <div
            className={classNames({
              "pl-7 py-4 hover:text-primary-light hover:bg-gray-900": true,
              "text-primary-light bg-gray-900": props.active === item.name,
            })}
          >
            <div>
              <Icon
                name={item.icon}
                className="mr-6"
                family={item.iconFamily}
              />
              {item.name}
            </div>
          </div>
        </Link>
      ))}
    </div>
  );
};
