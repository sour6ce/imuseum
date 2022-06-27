import classNames from 'classnames';
import React, { InputHTMLAttributes } from 'react';
import { FormError } from './FormError';
import { FormHelperText } from './FormHelperText';

export interface CheckboxProps
  extends InputHTMLAttributes<HTMLInputElement> {
  name: string;
  label?: string;
  placeholer?: string;
  required?: boolean;
  className?: string;
  error?: string;
  helperText?: string | React.ReactNode;
}

export const Checkbox: React.FC<CheckboxProps> = (props) => {
  const { className, ...inputProps } = props;

  const handleChange: React.ChangeEventHandler<HTMLInputElement> = (e) => {
    props.onChange?.(e);
  };

  return (
    <div
      className={classNames(
        className
      )}
    >
      <div className='flex items-center w-full gap-x-3'>
        <input
          className={classNames({
            'form-checkbox border-none focus-visible:border-none focus:border-none rounded-[3px] focus:ring-offset-side text-primary bg-side-dark h-5 w-5 focus:ring-primary': true,
          })}
          id={`label-${props.name}`}
          type={'checkbox'}
          checked={Boolean(props.value)}
          {...inputProps}
          onChange={handleChange}
        />
        <label
          className={classNames({
            'hidden': !props.label,
          })}
          htmlFor={`label-${props.name}`}
        >
          {props.label}
        </label>
      </div>
      <div className="ml-5 font-bold mb-3">
        {props.error ? (
          <FormError>{props.error}</FormError>
        ) : (
          <div>
            {props.helperText && (
              <FormHelperText>{props.helperText}</FormHelperText>
            )}
          </div>
        )}
      </div>
    </div>
  );
};
