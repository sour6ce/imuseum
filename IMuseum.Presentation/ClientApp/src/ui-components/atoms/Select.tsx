import { RefCallBack } from 'react-hook-form';
import ReactSelect, {
  ActionMeta,
  GroupBase,
  OnChangeValue,
  Props as ReactSelectProps,
  StylesConfig
} from 'react-select';
import AsyncReactSelect, {
  AsyncProps as AsyncReactSelectProps,
} from 'react-select/async';
import { FormError } from './FormError';
import { FormHelperText } from './FormHelperText';
import { FormLabel } from './FormLabel';
import classNames from 'classnames';

export interface BaseSelectProps{
  placeholder?: string;
  label?: string;
  required?: boolean;
  className?: string;
  name: string;
  containerClassName?: string;
  innerRef?: RefCallBack;
  helperText?: string | React.ReactNode;
  error?: string;
}

export type SelectProps<
  Option extends unknown,
  IsMulti extends boolean = false,
  Group extends GroupBase<Option> = GroupBase<Option>
> =
  | ({ async?: false } & BaseSelectProps &
      ReactSelectProps<Option, IsMulti, Group>)
  | ({ async: true } & BaseSelectProps &
      AsyncReactSelectProps<Option, IsMulti, Group>);

export const Select = <
  Option extends unknown,
  IsMulti extends boolean = false,
  Group extends GroupBase<Option> = GroupBase<Option>
>({
  containerClassName = '',
  className,
  innerRef,
  ...props
}: SelectProps<Option, IsMulti, Group>) => {
  const handleChange = (
    newValue: OnChangeValue<Option, IsMulti>,
    actionMeta: ActionMeta<Option>
  ) => {
    props.onChange?.(newValue, actionMeta);
  };
  const selectStyles : StylesConfig<Option,IsMulti,Group> = {
    control: (styles)=>({
      ...styles,
      backgroundColor: "#18212F",
      border: 'none',
      color: 'white',
      boxShadow: 'none',
      ':focus':{
        ...styles[':focus'],
        border: 'none',
        boxShadow: 'none'
      },
    }),
    input: (styles)=>({
      ...styles,
      color:'white'
    }),

    dropdownIndicator: (styles)=>({
      ...styles,
      color: '#9BBFD1',
      opacity: '0.5',
      ':hover':{
        opacity: '0.7',
      },
      ':focus':{
        opacity: '1'
      }
    }),
    placeholder: (styles)=>({
      ...styles,
      color: 'white',
      opacity: '0.6',
      fontWeight: 500
    }),
    indicatorSeparator: (styles)=>({
      ...styles,
      color: '#061B30',
      backgroundColor: '#061B30',
      opacity: '0.5'
    }),
    multiValueLabel: (styles)=>({
      ...styles,
      backgroundColor: '#9FBCD6',
      color:'white',
    }),
    multiValueRemove: (styles)=>({
      ...styles,
      backgroundColor: '#9FBCD6',
    }),
    clearIndicator: (styles)=>({
      ...styles,
      color: '#061B30',
      opacity: '0.5',
      ':hover':{
        opacity: '0.7',
      },
      ':focus':{
        opacity: '1'
      }
    }),
    menu: (styles)=>({
      ...styles,
      backgroundColor: '#18212F',
    }),
    singleValue: (styles)=>({
      ...styles,
      color: 'white',
    }),
    menuList: (styles)=>({
      ...styles,
      ':hover':{
        ...styles[':hover']??{},
        backgroundColor: '#18212F',
      }
    }),
    option:  (styles)=>({
      ...styles,
      ':hover':{
        ...styles[':hover']??{},
        backgroundColor: '#18212F',
      }
    }),
  }
  return (
    <div className={containerClassName}>
      <div className="flex items-center justify-between">
        {props.label && <FormLabel>{props.label}</FormLabel>}
      </div>
      {props.async ? (
        <AsyncReactSelect
          ref={innerRef}
          {...props}
          onChange={handleChange}
          classNamePrefix="select-prefix"
          className={classNames(className, 'react-select-container')}
          styles={selectStyles}
        />
      ) : (
        <ReactSelect
          ref={innerRef}
          {...props}
          onChange={handleChange}
          classNamePrefix="select-prefix"
          className={classNames(className, 'react-select-container')}
          styles={selectStyles}
        />
      )}
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


export default Select;
