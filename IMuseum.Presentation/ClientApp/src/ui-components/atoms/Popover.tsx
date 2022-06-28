import {Popover as P, Transition} from '@headlessui/react'
import classNames from 'classnames';
import { Fragment } from 'react'
import { Props } from '../../types/Props'
import { Button, ButtonProps } from './Button';


export interface PopoverProps extends Props {
  render: (params:{
      open:boolean,
      close:()=>void
    }) => React.ReactNode | string;
  buttonProps?: Omit<ButtonProps,'onClick'>;
  position: 'left'|'right'
}

export const Popover: React.FC<PopoverProps> = (props) => {
  const {
    className: buttonClassName,
    color: buttonColor,
    textColor: buttonTextColor,
    ...resButtonProps
  } = props?.buttonProps
  return (
    <P className="relative " as='div'>
      {({ open,close }:{open:boolean,close:()=>void}) => (
        <>
          <P.Button
          className={classNames('focus-visible:outline-none px-3 py-1.5 z-40 border-none ', {
            [`bg-${buttonColor}`]: buttonColor,
            [`text-${buttonTextColor}`]: buttonTextColor,
            'bg-primary': !buttonColor,
            'text-gray-50': !buttonTextColor,
          },buttonClassName ?? undefined)} 
          {...resButtonProps}
          as={Button}
          role="button"
          >
            {props.children}
          </P.Button>
          <Transition
            as={Fragment}
            enter="transition ease-out duration-200"
            enterFrom="opacity-0 translate-y-1"
            enterTo="opacity-100 translate-y-0"
            leave="transition ease-in duration-150"
            leaveFrom="opacity-100 translate-y-0"
            leaveTo="opacity-0 translate-y-1"
          >
            <P.Panel as='div' className={`absolute z-40 ${props.position === 'left' ? 'left-0' : 'right-0'} z-50 bg-gray-800 mt-1 w-screen shadow-xl ring-2 ring-gray-950 ring-opacity-5 rounded-lg max-w-md px-4 sm:px-0`}>
              <div className={props.className}>
                {props.render({open,close})}
              </div>
            </P.Panel>
          </Transition>
        </>
      )}
    </P>
  )
}


