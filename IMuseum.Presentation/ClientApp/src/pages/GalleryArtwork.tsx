import IconLogo from "../ui-components/atoms/IconLogo";
import { Popover } from "../ui-components/atoms/Popover";
import { NavLink, useLocation, useNavigate } from "react-router-dom";
import classNames from 'classnames'
import { Badge } from "../ui-components/atoms/Badge";
import { useSession } from "../hooks/useSession";
import { Button } from "../ui-components/atoms/Button";
import { useArtworkById, useArtworksPaginated } from "../hooks/useArtworks";

interface GalleryArtworkProps {
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

const GalleryArtwork: React.FC<GalleryArtworkProps> = (props) => {
  const location = useLocation()
  const {
    user,
    logout,
  } = useSession()
  const navigate = useNavigate()
  const {
    data
  } = useArtworkById(location.pathname.split('/')[2])
  
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
        </div>

        <div className="p-16 overflow-auto relative grid grid-cols-3 gap-5 items-start">
          <img src={data?.image} alt={data?.title} className='w-full'/>
          <div className="col-span-2 flex flex-col">
            <span className="text-4xl font-bold">{data?.title} <Badge
              className="text-lg ml-5"
            >
              {data?.status}
            </Badge></span>
            
            <div className="flex gap-4 text-lg mb-3 uppercase">
              <span>{data?.author}</span>
              <span className="text-primary-light">{data?.type}</span>
              <span className="text-primary-light">{data?.period}</span>
            </div>
            <span className="text font-bold text-gray-200 mt-5">
              Description
            </span>
            <span className="text-lg">{data?.description}</span>
          </div>
        </div>
      </div>
    </div>
  );
};

export default GalleryArtwork;
