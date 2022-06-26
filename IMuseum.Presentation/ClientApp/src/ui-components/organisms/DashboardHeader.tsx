import { Props } from "../../types/Props"
import { Button } from "../atoms/Button"
  import { Popover } from "../atoms/Popover"
import { Badge } from "../atoms/Badge"
import Icon from "../atoms/Icon"

export interface DashboardHeaderProps extends Props {
  title?: string,
  user?: any,
  onLogout?: () => void,
  activeNotification?:()=>void
}

export const DashboardHeader : React.FC<DashboardHeaderProps> = (props)=>{
  return (
    <header className="sticky text-gray-100 left-0 right-0 top-0 px-10 py-3 items-center  bg-gray-700  z-50 flex justify-between border-b-[2px] border-gray-800 ">
      <span className="text-xl">
        Dashboard
      </span>
      <div className="flex gap-x-3 flex-row items-center text-lg">
        <Button className="rounded-md bg-gray-400" textColor="primary-light" >
          <Icon family="fontawesome" name="bell" ></Icon>
        </Button>
        <span className="">
          Welcome, <span className="font-bold">{`${props.user?.first_name ?? ''} ${props.user?.last_name ?? ''}`} </span>
        </span>
        <Popover
          buttonProps={{
            color:"gray-400",
            textColor:"primary-light",
            className: 'rounded-md'
          }}
          render={({open,close})=>(
            <div className="bg-gray-700">
              <div className="flex justify-between p-5">
                <div className="flex flex-col gap-0">
                  <span className="text-lg font-semibold">{`${props.user?.first_name} ${props.user?.last_name} (${props.user?.username})`} </span>
                  <span className="text-xs font-semibold text-gray-100">{props.user?.email}</span>  
                  <div className="flex gap-2 mt-3 ">
                    {(props.user?.is_staff) ? <Badge color="primary-lighter" textColor="gray-100" className="text-xs uppercase self-start font-bold">Staff</Badge>:null}
                    {(props.user?.is_superuser)?<Badge color="primary" className="text-xs uppercase self-start font-bold">Superuser</Badge>:null}
                  </div>
                  <div className="mt-5">
                    <Button onClick={props.onLogout} className="py-1 text-base" >Logout</Button>
                  </div>
                </div>
                <div className="rounded-md bg-gray-400 w-16 h-16 flex justify-center items-center">
                  <div className="text-5xl font-semibold text-primary-light">{props.user?.first_name?.[0].toUpperCase()}</div>
                </div>
              </div>
            </div>

          )}
          position="right"
        >
          <span className="font-extrabold text-xl text-primary-light px-0.5">
          {props.user?.first_name?.[0].toUpperCase()}
          </span>
        </Popover>
      </div>
    </header>
  )
}

