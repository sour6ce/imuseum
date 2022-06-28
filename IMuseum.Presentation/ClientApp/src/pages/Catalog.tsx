import { useArtworksPaginated } from "../hooks/useArtworks";
import { Popover } from "../ui-components/atoms/Popover";
import ArtworkCard from "../ui-components/molecules/ArtworkCard"
import { Card, CardHeader } from "../ui-components/molecules/Card";

const Catalog =()=>{
  const {
    data,filters,handleChangeFilters,handleChangePagination,pagination
  } = useArtworksPaginated()
  return(
    
    <div>
      <Card className="">
        <CardHeader title="Catalog">
          <Popover
            render={({ open, close }) => (
              <div className="p-5">

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
                status: a.status+""
              }}/>
            </div>
            
          ))}
        </div>
      </Card>
      
    </div>
  )
}
export default Catalog;