import { Props } from "../../types/Props"
  import { Popover } from "../atoms/Popover"

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
        <span className="mr-3">
          Welcome, <span className="font-bold">{`${props.user?.first_name ?? 'John'} ${props.user?.last_name ?? 'Doe'}`} </span>
        </span>
        <Popover
          buttonProps={{
            color:"gray-400",
            textColor:"primary-light",
            className: 'rounded-md px-4 py-2'
          }}
          render={({open,close})=>(
            <div className="flex justify-between p-5">
              
            </div>
          )}
          position="right"
        >
          <span className="font-extrabold text-xl text-primary-light px-0.5">
          {props.user?.first_name?.[0].toUpperCase() ?? 'J'}
          </span>
        </Popover>
      </div>
    </header>
  )
}

