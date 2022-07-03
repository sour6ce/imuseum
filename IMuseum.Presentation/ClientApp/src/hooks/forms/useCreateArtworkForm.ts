import { useForm } from 'react-hook-form'
import {yupResolver} from '@hookform/resolvers/yup'
import * as yup from 'yup'
import dayjs from 'dayjs'
import { SelectOption } from '../../types/SelectOption'

const schema = yup.object({
  "title": yup.string().required(),
  "description": yup.string(),
  "author": yup.mixed<SelectOption<string>>().required(),
  "creationDate": yup.string().required(),
  "incorporatedDate": yup.string().required(),
  "period": yup.string().required(),
  "assessment": yup.number().required(),
  "image": yup.string().required(),
  "type": yup.mixed<SelectOption<string>>().required(),
  "style":yup.string(),
  "media": yup.string(),
  "material": yup.string(),
}).required()

export type NewArtworkForm = Partial<yup.InferType<typeof schema>>

export const useNewArtworkForm = ()=>{
  const methods = useForm<NewArtworkForm>({
    resolver: yupResolver(schema),
    defaultValues: {
      incorporatedDate: dayjs().format()
    }
  })
  return methods
}
