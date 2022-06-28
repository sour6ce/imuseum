import { useNavigate } from "react-router-dom"
import { useAuth } from "../../hooks/useAuth"
import { useSession } from "../../hooks/useSession"
import { Props } from "../../types/Props"
import { Badge } from "../atoms/Badge"
import { Button } from "../atoms/Button"
  import { Popover } from "../atoms/Popover"

export interface DashboardHeaderProps extends Props {
  title?: string,
  user?: any,
  onLogout?: () => void,
  activeNotification?:()=>void
}

export const DashboardHeader : React.FC<DashboardHeaderProps> = (props)=>{
  const {
    user,
    logout,
  } = useSession()
  const navigate = useNavigate()
  return (
    <header className="sticky text-gray-100 left-0 right-0 top-0 px-10 py-3 items-center  bg-gray-700  z-50 flex justify-between border-b-[2px] border-gray-800 ">
      <span className="text-xl">
        Dashboard
      </span>
      <div className="flex gap-x-3 flex-row items-center text-lg">
        <span className="mr-3">
          Welcome, <span className="font-bold">{`${user?.username ?? 'John'}`} </span>
        </span>
        <Popover
          buttonProps={{
            color:"gray-400",
            textColor:"primary-light",
            className: 'rounded-md px-4 py-2'
          }}
          render={({open,close})=>(
            <div className="flex flex-col justify-between p-5">
              <span className="text-2xl font-bold">{user?.username}</span>
              <span className="text-lg text-gray-200">{user?.email}</span>
              <div>
                <Badge textColor="gray-50" color="primary-lighter" >
                  {user?.role}
                </Badge>
              </div>
              <div className="mt-5">
                <Button
                  onClick={()=>{
                    logout()
                    navigate('/home')
                  }}
                  color='primary-light'
                >
                  Logout
                </Button>
              </div>
            </div>
          )}
          position="right"
        >
          <span className="font-extrabold text-xl text-primary-light px-0.5">
          {user?.username?.[0].toUpperCase() ?? 'J'}
          </span>
        </Popover>
      </div>
    </header>
  )
}

