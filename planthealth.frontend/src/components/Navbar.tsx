import Link from 'next/link'
import { buttonVariants } from '@/ui/Button'

const Navbar = async () => {
  return (
    <div className='fixed backdrop-blur-sm bg-white/75 z-50 top-0 left-0 right-0 h-10 border-b border-slate-300 shadow-sm flex items-center justify-between'>
      <div className='container max-w-7xl mx-auto w-full flex justify-between items-center'>
        <Link href='/' className={buttonVariants({ variant: 'link' })}>
          Home
        </Link>
      </div>
    </div>
  )
}

export default Navbar