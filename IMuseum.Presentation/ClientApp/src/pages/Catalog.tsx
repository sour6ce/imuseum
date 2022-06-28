import ArtworkCard from "../ui-components/molecules/ArtworkCard"
import { Card, CardHeader } from "../ui-components/molecules/Card";

const Catalog =()=>{

  const artworks=[
  {
    link:'',
    name:'The Creation',
    author:'Miguel Angel',
    type:'Painting',
    period:'Renaisance',
    description:'The painting shows the creatin of the first man Adan by God. Iconic artwork of the renaisance.',
    status:'exposed'
  },
  {
    link:'',
    name:'The Creation',
    author:'Miguel Angel',
    type:'Painting',
    period:'Renaisance',
    description:'The painting shows the creatin of the first man Adan by God. Iconic artwork of the renaisance.',
    status:'restoring'
  }
]
  return(
    
    <div>
      <Card className="">
        <CardHeader title="Catalog" />
        <div className="px-5 pt-5">
          {artworks.map((artwork)=>(
            <div className="pb-4">
              <ArtworkCard artwork={artwork}/>
            </div>
            
          ))}
        </div>
      </Card>
      
    </div>
  )
}
export default Catalog;