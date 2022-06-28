import { useForm } from 'react-hook-form'
import {yupResolver} from '@hookform/resolvers/yup'
import * as yup from 'yup'

const schema = yup.object({
  username: yup.string().required('Username is required'),
  password: yup.string().required('Password is required'),
}).required()

export type LoginFormValues = Partial<yup.InferType<typeof schema>>

export const useLoginForm = ()=>{
  const methods = useForm<LoginFormValues>({
    resolver: yupResolver(schema),
    defaultValues: {
      username: '',
      password: '',
    }
  })
  return methods
}
