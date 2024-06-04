'use client'
import Layout from '@/app/(pages)/Layout/Layout'
import Input from '@/component/input/Input';
import { AddEmailForgetPass } from '@/component/validation/validation';
import { Button } from '@mui/material';
import axios from 'axios';
import { useFormik } from 'formik';
import { useRouter } from 'next/navigation';
import '../RegisterCode/RegisterCode.css'

export default function page() {

    const router = useRouter();
    const initialValues={
        email: '',
    };


const onSubmit = async (users) => {
    try {
      const formData = new FormData();
      formData.append('email', users.email);
      const { data } = await axios.post('https://localhost:7116/api/UserAuth/AddEmailForForgetPassword', formData,{ headers: {
        'Content-Type': 'application/json',
      }});
      console.log(data);
      formik.resetForm();

    router.push(`/ForgetCode?email=${users.email}`,undefined, { shallow: true })

    } catch (error) {
      console.error('Error submitting form:', error);
      console.log('Error response:', error.response);
    }
  };



const formik = useFormik({
  initialValues : initialValues,
  onSubmit,
  validationSchema:AddEmailForgetPass
})
const inputs =[
 
    {
        type : 'email',
        id:'email',
        name:'email',
        title:'User Email',
        value:formik.values.email,
    },
]



const renderInputs = inputs.map((input,index)=>
  <Input type={input.type} 
        id={input.id}
         name={input.name}
          title={input.title} 
          key={index} 
          errors={formik.errors} 
          onChange={formik.handleChange}
           onBlur={formik.handleBlur}
            touched={formik.touched}
            />
        
    )


  return (
    <Layout>
      <div className="addEmailForget">
        <div className="container">
          <div className="row align-items-center">
            <div className="col-lg-6 pb-5">
              <div className="addEmailImg">
                <img
                  src="/addEmail2.png"
                  alt="add email "
                  className=" pb-5"
                />
              </div>
            </div>
            <div className="col-lg-6 pb-5">
              <div className="addEmailSection pt-3">
                <div className="title pb-4">
                  <h2 className='titleColor'>Insert your email here please</h2>
                </div>
                <div className="addEmailForm">
                  <form
                    onSubmit={formik.handleSubmit}
                    className="row justify-content-center align-items-center flex-column w-100"
                  >
                        {renderInputs}
                    
                    <div className="text-center mt-3">
                      <Button
                        sx={{ px: 2 }}
                        variant="contained"
                        className="m-2 btn primaryBg"
                        type="submit"
                        disabled={
                          formik.isSubmitting ||
                          Object.keys(formik.errors).length > 0 ||
                          Object.keys(formik.touched).length === 0
                        }
                      >
                        Add Email
                      </Button>
                    </div>
                  </form>
                </div>
              </div>
            </div>
            
          </div>
        </div>
      </div>
    </Layout>
  );
}
