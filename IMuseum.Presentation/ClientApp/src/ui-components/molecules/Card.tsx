import classNames from "classnames";
import { Props } from "../../types/Props";




export const Card : React.FC<Props> = (props)=>{
  return (
    <div className={classNames("bg-gray-700 w-full rounded-2xl",props.className)}>
      {props.children}
    </div>
  )
}

export interface CardHeaderProps extends Props {
  title: string,
  icon?: string | React.ReactElement,
}

export const CardHeader : React.FC<CardHeaderProps> = (props)=>{
  return (
    <div className="flex relative px-8 pb-3 pt-4 inset-x-0 top-0 bg-gray-900 rounded-t-2xl justify-between">
      <div className="flex gap-x-3 items-center">
        {!props.icon ? null : typeof props.icon === 'string' ? (
          <i className={props.icon}/>
        ) : (
          props.icon
        )}
        <span className="text-xl font-bold">
          {props.title}
        </span>
      </div>
      {props.children}
    </div>
  )
}

export const CardFooter : React.FC<Props> = (props)=>{
  return (
    <div className="relative inset-x-0 bottom-0 flex px-4 py-3 bg-gray-700">
      {props.children}
    </div>
  ) 
}
