import Dashboard from '@/components/Dashboard'

const page = async () => {
  return (
    <div className='max-w-fit mx-auto mt-16'>
       {/* @ts-expect-error Server Component */}
     <Dashboard/>
    </div>
  )
}

export default page