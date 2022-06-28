import { useForm } from 'react-hook-form'
import {yupResolver} from '@hookform/resolvers/yup'
import * as yup from 'yup'

const schema = yup.object({
  ArtworkId: yup.string(),
  MuseumId: yup.string(),
  IncomeMin: yup.number(),
  IncomeMax: yup.number(),
}).required()

export type LoanFormValues = Partial<yup.InferType<typeof schema>>

export const useLoanFilterForm = ()=>{
  const methods = useForm<LoanFormValues>({
    resolver: yupResolver(schema),
  })
  return methods
}
