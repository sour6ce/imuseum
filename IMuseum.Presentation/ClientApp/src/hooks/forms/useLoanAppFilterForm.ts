import { useForm } from 'react-hook-form'
import {yupResolver} from '@hookform/resolvers/yup'
import * as yup from 'yup'
import { Museum } from '../../types/Museum'
import { SelectOption } from '../../types/SelectOption'
import { Artwork } from '../../types/Artwork'

const schema = yup.object({
  ArtworkId: yup.mixed<Artwork>().required(),
  MuseumId: yup.mixed<Museum>().required(),
  Status: yup.mixed<SelectOption<string>>().required(),
}).required()

export type LoanAppFormValues = Partial<yup.InferType<typeof schema>>

export const useLoanAppFilterForm = ()=>{
  const methods = useForm<LoanAppFormValues>({
    resolver: yupResolver(schema),
  })
  return methods
}
