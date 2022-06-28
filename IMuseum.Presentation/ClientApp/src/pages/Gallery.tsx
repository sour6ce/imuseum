import IconLogo from "../ui-components/atoms/IconLogo";
import { Popover } from "../ui-components/atoms/Popover";
import { NavLink, useNavigate } from "react-router-dom";
import classNames from 'classnames'
import { Badge } from "../ui-components/atoms/Badge";
import { useSession } from "../hooks/useSession";
import { Button } from "../ui-components/atoms/Button";
import { useArtworksPaginated } from "../hooks/useArtworks";

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

  const {
    user,
    logout,
  } = useSession()
  const navigate = useNavigate()

  const {
    data,handleChangeFilters
  } = useArtworksPaginated({
    pagination:{
      pageSize:30,
    }
  })
  
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

        <div className="p-16 overflow-scroll grid grid-cols-3 gap-3 items-start">
          {data?.artworks.map((artwork) => <>{artwork.image ? (
            <img src={artwork.image} alt={artwork.title} className='w-full' onClick={()=>{
              navigate(`/gallery/${artwork.id}`)
            }}/>
          ) : null}</>)}
        </div>
      </div>
    </div>
  );
};

export default Gallery;
