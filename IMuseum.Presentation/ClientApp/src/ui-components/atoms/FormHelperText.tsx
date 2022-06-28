import classNames from 'classnames';
import { Props } from '../../types/Props';

export const FormHelperText: React.FC<Props> = (props) => {
  return (
    <small className={classNames('text-gray-200 ml-3', props.className)}>
      {props.children}
    </small>
  );
};
