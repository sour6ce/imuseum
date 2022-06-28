import { useForm } from 'react-hook-form'
import {yupResolver} from '@hookform/resolvers/yup'
import * as yup from 'yup'

const schema = yup.object({
  Author: yup.string(),
  Type: yup.string(),
  Statuses: yup.string(),
  Search: yup.string()
}).required()

export type CatalogFormValues = Partial<yup.InferType<typeof schema>>

export const useArtworkForm = ()=>{
  const methods = useForm<CatalogFormValues>({
    resolver: yupResolver(schema),
  })
  return methods
}
