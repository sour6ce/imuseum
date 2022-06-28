import classNames from "classnames";
import { ButtonHTMLAttributes, DetailedHTMLProps } from "react";
import { Props } from "../../types/Props";
import {Colors} from '../../utils/constants'



export interface ButtonProps extends Props, DetailedHTMLProps<
ButtonHTMLAttributes<HTMLButtonElement>,
HTMLButtonElement
> {
  color?: Colors;
  textColor?: Colors
  onClick?: ()=>void;
}

export const Button : React.FC<ButtonProps> = ({onClick,color,textColor,className,children,...props})=>{
  return (
    <button onClick={onClick} className={classNames({
      [`bg-${color}`]: color,
      [`text-${textColor}`]: textColor,
      'bg-gray-950': !color,
      'text-gray-50': !textColor,
      'px-3 py-1.5 rounded-sm': !className 
    },className)} {...props}>
      {children}
    </button>
  )
}
