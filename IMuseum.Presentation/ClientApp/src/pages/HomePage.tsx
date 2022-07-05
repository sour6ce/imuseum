import { NavLink } from "react-router-dom"
import classNames from 'classnames'
import { StaticService } from "../services/StaticService"
import { useAuth } from "../hooks/useAuth"
import { useEffect } from "react"
import { Popover } from "../ui-components/atoms/Popover"
import { Button } from "../ui-components/atoms/Button"
import { LoginForm } from "../ui-components/organisms/LoginForm"
import { Roles } from "../types/Roles"
import { Permission } from "../ui-components/atoms/Permission"


export const homeLinks = [
  {
    label: 'Home',
    to: '/home',
    roles: [Roles.Visiter]
  },
  {
    label: 'Gallery',
    to: '/gallery',
    roles: [Roles.Visiter]
  },
  {
    label: 'Dashboard',
    to: '/dashboard',
    roles: [Roles.Director,Roles.Manager,Roles.Restaurator]
  },
]

export const HomePage = () => {
  const {
    login,
    state,
  } = useAuth()
  return (
    <div  className={`absolute flex flex-col inset-0 overflow-hidden text-gray-50 bg-no-repeat bg-top lg:bg-right bg-cover lg:bg-contain p-5 bg-[#0E151F] bg-[url(/static/media/mobile.png)] lg:bg-[#0E151F] lg:bg-[url(/static/media/desk.png)]`}>
      <div className="flex flex-row justify-end gap-24">
        <div className="flex gap-8">
          {homeLinks.map((l)=>(
            <Permission
              roles={l.roles}
            >
              <NavLink to={l.to} className={({isActive})=>classNames('text-2xl hover:text-primary-lighter uppercase',{
                'font-black': isActive,
                'font-semibold': !isActive
              })}>{l.label}</NavLink>
            </Permission>
          ))}
        </div>
        <div>
          <Popover
            render={({open,close})=>(
              <div className="p-5 flex flex-col">
                <LoginForm/>
              </div>
            )}
            buttonProps={{}}
            position="right"
          >
            Login
          </Popover>
        </div>
      </div>
      <div className="flex flex-row">
        <div className="flex basis-full lg:basis-1/2">
          <div className="flex flex-col w-full">
            <img alt='logo' src='/logo-full.png' className="w-auto mx-10 lg:mx-24 my-32 lg:my-16 object-center object-contain"/>
            <div className="w-full px-16 flex flex-col gap-6 justify-center mt-24">
              <span className="text-4xl text-center font-semibold"><span className="font-bold uppercase text-primary-lighter mr-2">explore</span>the World!!!</span>
              <span className="text-4xl text-center font-semibold"><span className="font-bold uppercase text-primary-lighter mr-2">feel</span>the World!!!</span>
              <span className="text-4xl text-center font-semibold"><span className="font-bold uppercase text-primary-lighter mr-2">art</span>the World!!!</span>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}