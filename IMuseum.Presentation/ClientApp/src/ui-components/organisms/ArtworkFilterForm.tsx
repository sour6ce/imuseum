import { useArtworkForm } from '../../hooks/forms/useCatalogFilterForm'
import { GetArtworksFilter } from '../../services/ArtworkService.dto'
import { Button } from '../atoms/Button'
import { ControlledInput } from '../atoms/ControlledInput'
import { Form } from '../atoms/Form'

export interface CatalogFormProps {
  onSubmit: (data: GetArtworksFilter) => void
}

export const CatalogForm: React.FC<CatalogFormProps> = (props) => {
  const methods = useArtworkForm()
  return (
    <Form
      methods={methods}
      onSubmit={(data)=>{props.onSubmit({
        Author: [data.Author],
        Search: data.Search,
        Statuses: [data.Statuses],
        Type: [data.Type],
      })}}
      className="flex flex-col items-center justify-center p-5"
    >
      <div className='w-full grid grid-cols-1 py-2'>
        <ControlledInput
          name='Author'
          placeholder='Author'
          type='text'
          className='w-full'
        />
      </div>
      <div className=' w-full grid grid-cols-1 py-2'>
        <ControlledInput
          type='text'
          name='Type'
          placeholder='Type'
          className='w-full'
        />
      </div>
      <div className=' w-full grid grid-cols-1 py-2'>
        <ControlledInput
          type='text'
          name='Statuses'
          placeholder='Statuses'
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
