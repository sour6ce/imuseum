import { Input, InputProps } from './Input';
import React from 'react';
import { useController } from 'react-hook-form';

export const ControlledInput = <T extends unknown, P extends boolean = false>(
  props: InputProps<T, P>
) => {
  const {
    field: { onChange, ref, value, ...field },
    fieldState,
  } = useController({ name: props.name ?? '' });

  const formFieldProps = {
    ...field,
    value: value ?? '',
    error: fieldState.error?.message,
    onChange: ((e) => {
      (props.onChange as React.ChangeEventHandler)?.(e);
      onChange(e);
    }) as React.ChangeEventHandler,
  };

  const handleSelectChange = (e:any, meta:any) => {
    onChange({
      target: { name: props.name, value: e },
    });
    props.onChange?.(e, meta);
  };

  switch (props.type) {
    case 'select':
      return (
        <Input
          {...props}
          {...formFieldProps}
          type="select"
          innerRef={ref}
          onChange={handleSelectChange}
        />
      );
    case 'date':
    case 'number':
    case 'text':
    case 'checkbox':
    default:
      return <Input {...props} {...formFieldProps} />;
  }
};
