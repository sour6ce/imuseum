import classNames from 'classnames';
import { Props } from '../../types/Props';


export const FormError: React.FC<Props> = (props) => {
  return (
    <div
      className={classNames(
        'text-danger ml-2 font-medium mb-3 text-sm',
        props.className
      )}
    >
      <span role="alert">{props.children}</span>
    </div>
  );
};
