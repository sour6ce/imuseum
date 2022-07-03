import { useForm } from 'react-hook-form'
import {yupResolver} from '@hookform/resolvers/yup'
import * as yup from 'yup'
import { SelectOption } from '../../types/SelectOption'
import { Artwork } from '../../types/Artwork'
import { Museum } from '../../types/Museum'

const schema = yup.object({
  ArtworkId: yup.mixed<SelectOption<Artwork>>().required(),
  MuseumId: yup.mixed<SelectOption<Museum>>().required(),
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
