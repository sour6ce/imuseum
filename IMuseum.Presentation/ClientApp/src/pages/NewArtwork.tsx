import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { useNewArtworkForm } from "../hooks/forms/useCreateArtworkForm"
import { useAuth } from "../hooks/useAuth"
import { useSession } from "../hooks/useSession"
import { ArtworkService } from "../services/ArtworkService"
import { StaticService } from "../services/StaticService"
import { Button } from "../ui-components/atoms/Button"
import { ControlledInput } from "../ui-components/atoms/ControlledInput"
import { Form } from "../ui-components/atoms/Form"
import { Card } from "../ui-components/molecules/Card"



export const NewArtwork = ()=>{
  const navigate = useNavigate()
  const {
    user
  } = useSession()
  useEffect(()=>{
    if(user.role !== 'Catalog Manager'){
    navigate('/home')
  }})
  const methods = useNewArtworkForm()
  const [defaultOp, setDefaultOp] = useState<string[]>()
  useEffect(()=>{
    (StaticService.getArtists()).then((res)=>{
      setDefaultOp(res.artists)
    })
  },[])

  return (
    <Card className="flex flex-col p-5 gap-3">
      <Form
        methods={methods}
        onSubmit={data=>{
          ArtworkService.createArtwork({
            ...data,
            author: data.author.value,
          })
        }}
        className=" gap-3 grid grid-cols-2"
      >
        <ControlledInput
          name="title"
          type="text"
          placeholder="Title"
          label="Title"
          containerClassName="w-full"
        />
        <ControlledInput
          name="description"
          type="text"
          placeholder="Description"
          label="Description"
        />
        {/*select de autores*/}
        <ControlledInput
          type={'select'}
          options={defaultOp?.map(o=>({
            label: o,
            value: o
          }))}
          name="author"
          label="Author"
          placeholder={'Author'}
          containerClassName=""
        />
        <ControlledInput
          name="creationDate"
          type="date"
          label="Creation Date"
        />
        <ControlledInput
          name="period"
          type="text"
          placeholder="Period"
          label="Period"
        />
        <ControlledInput
        name="incorporatedDate"
        type="date"
        label="Incorporated Date"
        />
        <ControlledInput
          name="assessment"
          type="number"
          placeholder="assessment"
          label="assessment"
        />
        <ControlledInput
          name="type"
          type="text"
          placeholder="type"
          label="type"
        />
        <ControlledInput
          name="image"
          type="text"
          placeholder="image"
          label="image"
        />
        <ControlledInput
          name="style"
          type="text"
          placeholder="style"
          label="style"
        />
        <ControlledInput
          name="media"
          type="text"
          placeholder="media"
          label="media"
        />
        <ControlledInput
          name="material"
          type="text"
          placeholder="material"
          label="material"
        />
        <div className="mt-10">
        <Button
          type="submit"
        >
          Create
        </Button>
        </div>
      </Form>
    </Card>
  )
}
