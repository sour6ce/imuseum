import { Props } from "../../types/Props";
import { DashboardHeader } from "../organisms/DashboardHeader";
import { AsideMenu } from "../organisms/AsideMenu";
import DashboardSubheader, {DashboardSubheaderProps} from "../organisms/DashboardSubheader";
import { Outlet } from "react-router-dom";

export interface DashboardLayoutProps extends Props,Pick<DashboardSubheaderProps,'title'>{
 activeItem:string

}

export const DashboardLayout : React.FC<DashboardLayoutProps> = (props)=>{
  return (
      <div className="absolute inset-0 flex flex-row bg-gray-600 text-gray-100 overflow-hidden">
        <AsideMenu active={props.activeItem} items={
                [{icon:"home", iconFamily:"fontawesome", name:"Dashboard", to:"/dashboard"},
                {icon:"image", iconFamily:"fontawesome", name:"Gallery", to:"/gallery"},
                {icon:"user", iconFamily:"fontawesome", name:"Users", to:"/users"},
                {icon:"book", iconFamily:"fontawesome", name:"Catalog", to:"/catalog"},
                {icon:"gear", iconFamily:"fontawesome", name:"Restoration", to:"/restoration"},
                {icon:"handshake-angle", iconFamily:"fontawesome", name:"Loans ", to:"/loans"},
                {icon:"list-check", iconFamily:"fontawesome", name:"Loans Application", to:"/loans-application"}]}
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
