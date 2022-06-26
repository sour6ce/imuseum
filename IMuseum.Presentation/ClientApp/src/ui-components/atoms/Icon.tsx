import classNames from "classnames";
import { Colors } from "../../utils/constants";

export interface IconProps {
  name: string
  color?: Colors,
  family:IconFamily,
  className?:string
}

export type IconFamily = "bootstrap" | "fontawesome"

export const Icon:React.FC<IconProps> = (props) => {
    return(<i className={classNames({
      "fas":props.family==="fontawesome",
      [`fa-${props.name}`]: props.name && props.family==="fontawesome",
      "bi":props.family==="bootstrap",
      [`bi-${props.name}`]:props.name && props.family==="bootstrap",
      [`text-${props.color}`]:props.color,
      },props.className)}></i>)
      
};
export default Icon;