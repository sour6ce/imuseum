import { Props } from "../../types/Props";
import { DashboardHeader } from "../organisms/DashboardHeader";
import { AsideMenu } from "../organisms/AsideMenu";
import DashboardSubheader, {DashboardSubheaderProps} from "../organisms/DashboardSubheader";
import { Outlet, useLocation } from "react-router-dom";
import { IconFamily } from "../atoms/Icon";
import { Roles } from "../../types/Roles";

export interface DashboardLayoutProps extends Props,Pick<DashboardSubheaderProps,'title'>{
}

export const asideItems : {
  icon:string;
  name:string;
  to:string;
  iconFamily:IconFamily;
  roles:Roles[];
}[] = 
[
  {icon:"home", iconFamily:"fontawesome", name:"Home", to:"/home",roles:[Roles.Visiter]},
  {icon:"grip", iconFamily:"fontawesome", name:"Dashboard", to:"/dashboard",roles:[Roles.Visiter]},
  {icon:"image", iconFamily:"fontawesome", name:"Gallery", to:"/gallery",roles:[Roles.Visiter]},
  {icon:"book", iconFamily:"fontawesome", name:"Catalog", to:"/catalog",roles:[Roles.Manager]},
  {icon:"gear", iconFamily:"fontawesome", name:"Restoration", to:"/restoring",roles:[Roles.Restaurator]},
  {icon:"handshake-angle", iconFamily:"fontawesome", name:"Loans ", to:"/loans",roles:[Roles.Director]},
  {icon:"list-check", iconFamily:"fontawesome", name:"Loan Applications", to:"/loan-applications",roles:[Roles.Director]}
]

export const DashboardLayout : React.FC<DashboardLayoutProps> = (props)=>{
  const location = useLocation()
  return (
      <div className="absolute inset-0 flex flex-row bg-gray-600 text-gray-100 overflow-hidden">
        <AsideMenu active={asideItems.find(i => i.to === location.pathname)?.name} items={asideItems}
        />
        <div className="flex flex-col w-full flex-grow h-full overflow-hidden">
          <div className="w-full">
            <DashboardHeader
              title={props.title}
            />
            <DashboardSubheader
              title={props.title}
            />
          </div>
          <div className="w-full flex justify-center h-full overflow-auto w-100 px-8 py-5">
              <Outlet/>
          </div>
        </div>

    </div>
  )
}
