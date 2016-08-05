var ChangePassword = function () {

    var handleChangePassword = function () {

        $('.changepassword-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            rules: {
                //自定义规则，key:value 的形式，key 是要验证的元素，value 可以是字符串或对象。
                oldpassword: {
                    required: true
                },
                newpassword: {
                    required: true,
                    maxlength:6
                },
                confirmpassword: {
                    equalTo: "input[name=newpassword]",
                    onkeyup: true
                }
            },

            messages: {
                //自定义的提示信息，key:value 的形式，key 是要验证的元素，value 可以是字符串或函数。
                oldpassword: {
                    required: '原始密码不能为空'
                },
                newpassword: {
                    required: '新密码不能为空'
                },
                confirmpassword: {
                    required: '确认密码不能为空',
                    equalTo: "两次密码输入不一致"
                }
            },

            invalidHandler: function (event, validator) { //display error alert on form submit   
                
            },

            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            success: function (label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove(); 
            },

            errorPlacement: function (error, element) {
                error.insertAfter(element.closest('.input-icon'));
            },

            submitHandler: function (form) {
                //通过验证后运行的函数，里面要加上表单提交的函数，否则表单不会提交。
                form.submit(); // form validation success, call ajax form submit
            }
        });

        $('.changepassword-form input').keypress(function (e) {
            if (e.which == 13) {
                if ($('.changepassword-form').validate().form()) {
                    $('.changepassword-form').submit(); //form validation success, call ajax form submit
                }
                return false;
            }
        });
    }

    return {
        //main function to initiate the module
        init: function () {

            handleChangePassword();
        }

    };

}();

jQuery(document).ready(function () {
    ChangePassword.init();
});

//function changePassword() {
//    //验证数据

//    $.ajax({
//        method: 'post',
//        url: 'account/changepassword',
//        dataType: 'json',
//        data: {
//            oldpassword: $('#oldpassword').val(),
//            newpassword: $('#newpassword').val(),
//            confirmpassword: $('#confirmpassword').val()
//        },
//        success: function (data) {
//            alert(data.Message);
//            //if (data.IsSuccess)
//            //{

//            //}
//        }
//    });
//}
