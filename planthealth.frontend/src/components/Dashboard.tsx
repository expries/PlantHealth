import LargeHeading from '@/ui/LargeHeading'
import Paragraph from "@/ui/Paragraph"
import Table from "@/ui/Table"

const Dashboard = async ({}) => {
    const baseUrl = process.env.API_BASE_URL
    const data = await fetch(`${baseUrl}/SensorData`)
    const result = await data.json()

    return (
      <div className='container flex flex-col gap-6'>
        <LargeHeading>PlantHealth Sensor Data</LargeHeading>
  
        <Paragraph className='text-center md:text-left mt-4 -mb-4'>
          Sensor Data:
        </Paragraph>
  
        <Table sensorDataModels={result} redirectOnClick={true} />
      </div>
    )
  }
  
  export default Dashboard