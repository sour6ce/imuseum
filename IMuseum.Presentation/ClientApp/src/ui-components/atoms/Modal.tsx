import { Dialog, Transition } from '@headlessui/react'
import classNames from 'classnames'
import { Fragment } from 'react'
import { Props } from '../../types/Props'


export interface ModalProps extends Props {
  isOpen?: boolean,
  hanldeModalClose: () => void,
  size?: 'sm' | 'md' | 'lg' | 'xl' | '2xl' | '3xl',
}

export const Modal : React.FC<ModalProps>  = (props) => {

  return (
    <Transition appear show={props?.isOpen} as={Fragment}>
      <Dialog as="div" className="relative z-50" onClose={props.hanldeModalClose}>
        <Transition.Child
          as={Fragment}
          enter="ease-out duration-300"
          enterFrom="opacity-0"
          enterTo="opacity-100"
          leave="ease-in duration-200"
          leaveFrom="opacity-100"
          leaveTo="opacity-0"
        >
          <div className="fixed inset-0 bg-black bg-opacity-40" />
        </Transition.Child>

        <div className="fixed inset-0 overflow-y-auto">
          <div className="flex min-h-full items-center justify-center p-4 text-center">
            <Transition.Child
              as={Fragment}
              enter="ease-out duration-300"
              enterFrom="opacity-0 scale-95"
              enterTo="opacity-100 scale-100"
              leave="ease-in duration-200"
              leaveFrom="opacity-100 scale-100"
              leaveTo="opacity-0 scale-95"
            >
              <Dialog.Panel 
                className={classNames("w-full transform overflow-hidden rounded-lg bg-white p-6 text-left align-middle shadow-xl transition-all",{
                  [`max-w-${props.size}`]: props.size,
                  'max-w-md': !props.size,
                },props.className)}>
                {props.children}
              </Dialog.Panel>
            </Transition.Child>
          </div>
        </div>
      </Dialog>
    </Transition>
  )
}
