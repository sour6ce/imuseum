import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useArtworksPaginated } from "../hooks/useArtworks";
import { useSession } from "../hooks/useSession";
import { Button } from "../ui-components/atoms/Button";
import { Popover } from "../ui-components/atoms/Popover";
import { Portal } from "../ui-components/atoms/Portal";
import ArtworkCard from "../ui-components/molecules/ArtworkCard"
import { Card, CardHeader } from "../ui-components/molecules/Card";
import { CatalogForm } from "../ui-components/organisms/ArtworkFilterForm";

const Catalog =()=>{
  const navigate = useNavigate()
  const {
    user
  } = useSession()
  useEffect(()=>{
    if(user.role !== 'Catalog Manager'){
    navigate('/home')
  }})
  const {
    data,handleChangeFilters,
    mutate
  } = useArtworksPaginated()
  return(
      <Card className="w-full">
        <Portal portalName="subheader-portal-container">
          <Button
            onClick={()=>{
              navigate('/catalog/new')
            }}
          >
            Create Artwork
          </Button>
        </Portal>
        <CardHeader title="Catalog">
          <Popover
            render={({ open, close }) => (
              <div className="p-5">
                <CatalogForm onSubmit={handleChangeFilters}/>
              </div>
            )}
            buttonProps={{}}
            position="right"
          >
            Filter
          </Popover>
        </CardHeader>
        <div className="px-5 pt-5">
          {data?.artworks?.map((a)=>(
            <div className="pb-4">
              <ArtworkCard artwork={{
                author: a.author,
                name: a.title,
                type: a.type+"",
                description: a.description,
                link: a.image,
                period: a.period,
                status: a.status+"",
                id: a.id,
                room: a.room
              }}
              mutate={mutate}/>
            </div>
            
          ))}
        </div>
      </Card>
  )
}
export default Catalog;