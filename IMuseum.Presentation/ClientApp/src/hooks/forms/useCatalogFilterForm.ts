import { useForm } from 'react-hook-form'
import {yupResolver} from '@hookform/resolvers/yup'
import * as yup from 'yup'
import { SelectOption } from '../../types/SelectOption'

const schema = yup.object({
  Author: yup.mixed<SelectOption<string>[]>().required(),
  Type: yup.mixed<SelectOption<string>[]>().required(),
  Statuses: yup.mixed<SelectOption<string>[]>().required(),
  Search: yup.string()
}).required()

export type CatalogFormValues = Partial<yup.InferType<typeof schema>>

export const useArtworkForm = ()=>{
  const methods = useForm<CatalogFormValues>({
    resolver: yupResolver(schema),
  })
  return methods
}
