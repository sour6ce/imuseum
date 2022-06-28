import classNames from 'classnames';
import React, { InputHTMLAttributes } from 'react';
import { FormError } from './FormError';
import { FormHelperText } from './FormHelperText';
import { FormLabel } from './FormLabel';

export interface FormComponent {
  helperText?: string | React.ReactNode;
  error?: string;
  label?: string;
}

export type IconPosition = 'left' | 'right';

export interface BaseInputProps
  extends Omit<InputHTMLAttributes<HTMLInputElement>, 'size'>,
    FormComponent {
  label?: string;
  labelRight?: React.ReactNode;
  decoratorRight?: React.ReactNode;
  required?: boolean;
  icon?: React.ReactNode | string;
  iconPosition?: IconPosition;
  containerClassName?: string;
}

export const BaseInput = React.forwardRef<HTMLInputElement, BaseInputProps>(
  (
    {
      type = 'text',
      name,
      label,
      placeholder = label,
      className,
      required,
      helperText,
      containerClassName,
      icon,
      iconPosition = 'right',
      ...props
    },
    ref
  ) => {
    const { labelRight, decoratorRight, ...inputProps } = props;

    const handleChange: React.ChangeEventHandler<HTMLInputElement> = (e) => {
      props.onChange?.(e);
    };

    return (
      <div className={classNames(containerClassName)}>
        <div className="flex items-center justify-between">
          {label && <FormLabel htmlFor={`label-${name}`} required={required}>{label}</FormLabel>}
          {labelRight}
        </div>
        <div className="flex items-center relative">
          {icon && (
            <div
              className={classNames('absolute', {
                'right-0 mr-5': iconPosition === 'right',
                'left-0 ml-5': iconPosition === 'left',
              })}
            >
              {typeof icon === 'string' ? <i className={icon}/> : icon}
            </div>
          )}
          <input
            ref={ref}
            id={`label-${name}`}
            type={type}
            className={classNames('form-input bg-gray-600 text-gray-100 text-opacity-80 font-medium border-none rounded-md',
            'focus:ring-0',
            'placeholder:text-gray-100 placeholder:opacity-50 placeholder:font-medium',
            'invalid:ring-2 invalid:ring-danger invalid:ring-opacity-50',
            className, {
              'pl-14': icon && iconPosition === 'left',
              'pr-14': icon && iconPosition === 'right',
            })}
            placeholder={placeholder}
            {...inputProps}
            onChange={handleChange}
          />
          {decoratorRight}
        </div>
        {(props.error || helperText) &&
          (props.error ? (
            <FormError>{props.error}</FormError>
          ) : (
            <FormHelperText>{helperText}</FormHelperText>
          ))}
      </div>
    );
  }
);

BaseInput.displayName = 'BaseInput';

export default BaseInput;
