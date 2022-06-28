import classNames from "classnames"
import { Props } from "../../types/Props"
import { Colors } from "../../utils/constants"



export interface BadgeProps extends Props{
  color?: Colors,
  textColor?: Colors
  onClick?: ()=>void
}

export const Badge: React.FC<BadgeProps> = (props) => {
  return (
    <span className={classNames('px-2 py-0.5 cursor-pointer rounded-md', {
      [`bg-${props.color}`]: props.color,
      [`text-${props.textColor}`]: props.textColor,
      'bg-gray-950': !props.color,
      'text-gray-50': !props.textColor,
    },props.className)}
      onClick={props.onClick}
    >
      {props.children}
    </span>
  )
}
