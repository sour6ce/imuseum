import { useNavigate } from 'react-router-dom'
import yup from 'yup'
import { LoginFormValues, useLoginForm } from '../../hooks/forms/useLoginForm'
import { useAuth } from '../../hooks/useAuth'
import { Button } from '../atoms/Button'
import { ControlledInput } from '../atoms/ControlledInput'
import { Form } from '../atoms/Form'


export const LoginForm: React.FC = (props) => {
  const methods = useLoginForm()
  const {login} = useAuth()
  const nav = useNavigate()
  return (
    <Form
      methods={methods}
      onSubmit={(data)=>{login(data as LoginFormValues).then(()=>{
        nav('/dashboard')
      })}}
      className="flex flex-col items-center justify-center p-5"
    >
      <div className='font-extrabold text-3xl self-start'>
        Welcome
      </div>
      <div className='font-bold text-base self-start mb-6'>
        To IMuseum
      </div>
      <div className='w-full grid grid-cols-1 py-2'>
        <ControlledInput
          name='username'
          placeholder='Username'
          type='text'
          className='w-full'
        />
      </div>
      <div className=' w-full grid grid-cols-1 py-2'>
        <ControlledInput
          type='password'
          name='password'
          placeholder='Password'
          className='w-full'
        />
      </div>
      <div className='w-full flex flex-row justify-start py-2'>
        <Button
          color='primary-light'
          textColor='gray-100'
          type='submit'
          className='rounded-sm mt-3'
          onClick={()=>{return}}
        >
          Login
        </Button>
      </div>
    </Form>
  )
}
