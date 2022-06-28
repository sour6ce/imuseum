import { useArtworkForm } from '../../hooks/forms/useCatalogFilterForm'
import { GetRestorationsFilter } from '../../services/RestoreService.dto'
import { Button } from '../atoms/Button'
import { ControlledInput } from '../atoms/ControlledInput'
import { Form } from '../atoms/Form'
import dayjs from 'dayjs'
import { useRestorationForm } from '../../hooks/forms/useRestorationFilterForm'

export interface RestorationFilterFormProps {
  onSubmit: (data: GetRestorationsFilter) => void
}

export const RestorationFilterForm: React.FC<RestorationFilterFormProps> = (props) => {
  const methods = useRestorationForm()
  return (
    <Form
      methods={methods}
      onSubmit={(data)=>{props.onSubmit({
        ArtworksIds: [data.ArtworksIds],
        EndDateA: data.EndDateA ? dayjs(data.EndDateA).format('YYYY-MM-DD') : undefined,
        EndDateB: data.EndDateB ? dayjs(data.EndDateB).format('YYYY-MM-DD') : undefined,
        StartDateA: data.StartDateA ? dayjs(data.StartDateA).format('YYYY-MM-DD') : undefined,
        StartDateB: data.StartDateB ? dayjs(data.StartDateB).format('YYYY-MM-DD') : undefined,
      })}}
      className="flex flex-col items-center justify-center p-5"
    >
      <div className='w-full grid grid-cols-1 py-2'>
        <ControlledInput
          name='ArtworksIds'
          placeholder='ArtworksId'
          type='text'
          className='w-full'
        />
      </div>
      <div className=' w-full grid grid-cols-2 py-2'>
        <ControlledInput
          type='date'
          name='StartDateA'
          label='Start Date After'
          className='w-full'
        />
        <ControlledInput
          type='date'
          name='StartDateB'
          label='Start Date Before'
          className='w-full'
        />
      </div>
      <div className=' w-full grid grid-cols-2 py-2'>
        <ControlledInput
          type='date'
          name='EndDateA'
          label='End Date After'
          className='w-full'
        />
        <ControlledInput
          type='date'
          name='EndDateB'
          label='End Date Before'
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
