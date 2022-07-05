import { useEffect, useState } from 'react'
import { useArtworkForm } from '../../hooks/forms/useCatalogFilterForm'
import { GetArtworksFilter } from '../../services/ArtworkService.dto'
import { StaticService } from '../../services/StaticService'
import { Button } from '../atoms/Button'
import { ControlledInput } from '../atoms/ControlledInput'
import { Form } from '../atoms/Form'

export interface CatalogFormProps {
  onSubmit: (data: GetArtworksFilter) => void
}

export const CatalogForm: React.FC<CatalogFormProps> = (props) => {
  const methods = useArtworkForm()

  const [defaultOp, setDefaultOp] = useState<string[]>()
  useEffect(()=>{
    (StaticService.getArtists()).then((res)=>{
      setDefaultOp(res)
    })
  },[])
  return (
    <Form
      methods={methods}
      onSubmit={(data)=>{props.onSubmit({
        Author: data?.Author?.map(a=>a.value),
        Search: data.Search,
        Statuses: data?.Statuses?.map(s=>s.value),
        Type: data?.Type?.map(t=>t.value),
      })}}
      className="flex flex-col items-center justify-center p-5"
    >
      <div className='w-full grid grid-cols-1 py-2'>
        
      <ControlledInput
          isMulti
          isClearable
          type={'select'}
          options={defaultOp?.map(o=>({
            label: o,
            value: o
          }))}
          name="Author"
          label="Author"
          placeholder={'Author'}
          containerClassName=""
        />
      </div>
      <div className=' w-full grid grid-cols-1 py-2'>
        <ControlledInput
          isMulti
          isClearable
          type='select'
          name='Type'
          placeholder='Type'
          label='Type'
          options={[
            {
              label: 'Painting',
              value: 'painting'
            },
            {
              label:'Sculpture',
              value:'sculpture'
            },
            {
              label: 'Other',
              value:'other'
            }
          ]}
          className='w-full'
        />
      </div>
      <div className=' w-full grid grid-cols-1 py-2'>
      <ControlledInput
          isMulti
          isClearable
          type='select'
          name='Statuses'
          placeholder='Statuses'
          label='Statuses'
          options={[
            {
              label: 'On Loan',
              value: 'on-loan'
            },
            {
              label:'In Restoration',
              value:'in-restoration'
            },
            {
              label: "In Storage",
              value:"in-storage"
            },
            {
              label: "On Display",
              value:"on-display"
            },
            {
              label: 'Out',
              value:'out'
            }
          ]}
          className='w-full'
        />
      </div>
      <div className=' w-full grid grid-cols-1 py-2'>
        <ControlledInput
          type='text'
          name='Search'
          placeholder='Search'
          className='w-full'
        />
      </div>
      <div className='w-full flex flex-row justify-start py-2'>
        <Button
          type='submit'
          onClick={()=>{return}}
        >
          Filter
        </Button>
      </div>
    </Form>
  )
}
