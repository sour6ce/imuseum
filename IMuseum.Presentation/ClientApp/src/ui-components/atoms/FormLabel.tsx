import classNames from 'classnames';

export interface FormLabelProps extends React.HTMLProps<HTMLLabelElement> {
  className?: string;
  required?: boolean;
}

export const FormLabel: React.FC<FormLabelProps> = (props) => {
  return (
    <label
      className={classNames(`fw-bold text-gray-100 text-sm`,
      props.className, {
        "after:content-['*']": props.required,
      })}
      {...props}
    >
      {props.children}
    </label>
  );
};
