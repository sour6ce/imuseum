import classNames from 'classnames';
import React, { HTMLAttributes } from 'react';
import { DetailedHTMLProps } from 'react';
import {
  FieldValues,
  FormProvider,
  SubmitHandler,
  UseFormReturn,
} from 'react-hook-form';

export interface FormProps
  extends DetailedHTMLProps<HTMLAttributes<HTMLFormElement>, HTMLFormElement> {
  methods: UseFormReturn<FieldValues, object>;
  children: React.ReactNode;
  className?: string;
  onSubmit: SubmitHandler<FieldValues>;
}
export function Form({
  methods,
  className,
  onSubmit,
  children,
  ...props
}: FormProps) {
  return (
    <FormProvider {...methods}>
      <form
        {...props}
        onSubmit={methods.handleSubmit(onSubmit)}
        onReset={() => methods.reset()}
        className={classNames(className)}
      >
        {children}
      </form>
    </FormProvider>
  );
}
