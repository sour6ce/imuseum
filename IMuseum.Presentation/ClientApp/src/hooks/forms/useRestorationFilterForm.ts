import { useForm } from 'react-hook-form'
import {yupResolver} from '@hookform/resolvers/yup'
import * as yup from 'yup'

const schema = yup.object({
  ArtworksIds: yup.string(),
  StartDateA: yup.string(),
  EndDateA: yup.string(),
  StartDateB: yup.string(),
  EndDateB: yup.string(),
}).required()

export type RestorationFormValues = Partial<yup.InferType<typeof schema>>

export const useRestorationForm = ()=>{
  const methods = useForm<RestorationFormValues>({
    resolver: yupResolver(schema),
  })
  return methods
}
