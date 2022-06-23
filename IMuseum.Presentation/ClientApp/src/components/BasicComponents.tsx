import { Outlet } from "react-router-dom"

export interface BasicComponentProps {
  text: string
}

export const BasicComponent : React.FC<BasicComponentProps> = (props)=>{
  return (
    <div>
      {props.text}
      <Outlet/>
    </div>
  )
}