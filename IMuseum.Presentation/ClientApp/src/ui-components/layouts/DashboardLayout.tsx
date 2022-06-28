import { Props } from "../../types/Props";
import { DashboardHeader } from "../organisms/DashboardHeader";
import { AsideMenu } from "../organisms/AsideMenu";
import DashboardSubheader, {DashboardSubheaderProps} from "../organisms/DashboardSubheader";
import { Outlet, useLocation } from "react-router-dom";
import { IconFamily } from "../atoms/Icon";

export interface DashboardLayoutProps extends Props,Pick<DashboardSubheaderProps,'title'>{
}

export const asideItems : {
  icon:string;
  name:string;
  to:string;
  iconFamily:IconFamily;
}[] = 
[
  {icon:"home", iconFamily:"fontawesome", name:"Home", to:"/home"},
  {icon:"grip", iconFamily:"fontawesome", name:"Dashboard", to:"/dashboard"},
  {icon:"image", iconFamily:"fontawesome", name:"Gallery", to:"/gallery"},
  {icon:"book", iconFamily:"fontawesome", name:"Catalog", to:"/catalog"},
  {icon:"gear", iconFamily:"fontawesome", name:"Restoration", to:"/restoring"},
  {icon:"handshake-angle", iconFamily:"fontawesome", name:"Loans ", to:"/loans"},
  {icon:"list-check", iconFamily:"fontawesome", name:"Loan Applications", to:"/loan-applications"}
]

export const DashboardLayout : React.FC<DashboardLayoutProps> = (props)=>{
  const location = useLocation()
  console.log(location)
  return (
      <div className="absolute inset-0 flex flex-row bg-gray-600 text-gray-100 overflow-hidden">
        <AsideMenu active={asideItems.find(i => i.to === location.pathname).name} items={asideItems}
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
