import SensorDetailDashboard from "@/components/SensorDetailDashboard"


interface IProps{
    params: {
      serialNumber: string
    }
  }
  
const page = async ({params}: IProps) => {
    const {serialNumber} = params

    return (
        <div className='max-w-fit mx-auto mt-16'>
            {/* @ts-expect-error Server Component */}
            <SensorDetailDashboard serialNumber = {serialNumber}/>
        </div>
    )
}

export default page