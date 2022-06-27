import React from 'react';
import { BaseInput, BaseInputProps } from '../atoms/BaseInput';
import { Checkbox, CheckboxProps } from '../atoms/Checkbox';
import Select, { SelectProps } from '../atoms/Select';

export type InputProps<T, P extends boolean = false> =
  | ({ type?: 'text' } & BaseInputProps)
  | ({ type: 'number' } & BaseInputProps)
  | ({ type: 'select' } & SelectProps<T, P>)
  | ({ type: 'checkbox' } & CheckboxProps)
  | ({ type: 'date' } & BaseInputProps)
  | ({ type: 'password' } & BaseInputProps)
  | ({ type: 'email' } & BaseInputProps);

export const Input = <T extends unknown, P extends boolean = false>({
  ...props
}: InputProps<T, P>) => {
  switch (props.type) {
    case 'select': {
      return <Select {...props} />;
    }
    case 'checkbox': {
      return <Checkbox {...props} />;
    }
    case 'text':
    case 'number':
    case 'email':
    default: {
      return <BaseInput {...props} />;
    }
  }
};

export default Input;
