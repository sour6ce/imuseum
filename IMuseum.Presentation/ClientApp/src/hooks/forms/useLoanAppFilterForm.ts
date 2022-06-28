import { useForm } from 'react-hook-form'
import {yupResolver} from '@hookform/resolvers/yup'
import * as yup from 'yup'

const schema = yup.object({
  ArtworkId: yup.string(),
  MuseumId: yup.string(),
  Status: yup.string(),
}).required()

export type LoanAppFormValues = Partial<yup.InferType<typeof schema>>

export const useLoanAppFilterForm = ()=>{
  const methods = useForm<LoanAppFormValues>({
    resolver: yupResolver(schema),
  })
  return methods
}
