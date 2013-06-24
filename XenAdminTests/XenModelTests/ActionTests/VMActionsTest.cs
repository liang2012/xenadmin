﻿/* Copyright (c) Citrix Systems Inc. 
 * All rights reserved. 
 * 
 * Redistribution and use in source and binary forms, 
 * with or without modification, are permitted provided 
 * that the following conditions are met: 
 * 
 * *   Redistributions of source code must retain the above 
 *     copyright notice, this list of conditions and the 
 *     following disclaimer. 
 * *   Redistributions in binary form must reproduce the above 
 *     copyright notice, this list of conditions and the 
 *     following disclaimer in the documentation and/or other 
 *     materials provided with the distribution. 
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND 
 * CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE 
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF 
 * SUCH DAMAGE.
 */

using XenAdmin.Actions.VMActions;
using NUnit.Framework;
using XenAPI;

namespace XenAdminTests.XenModelTests.ActionTests
{
    [TestFixture, Category(TestCategories.UICategoryA)]
    public class CreateVMFastActionTest : ActionTest<CreateVMFastAction>
    {
        protected override CreateVMFastAction NewAction()
        {
            VM template = GetAnyUserTemplate(VmIsInstant);
            return new CreateVMFastAction(template.Connection, template);
        }

        protected override bool VerifyResult(CreateVMFastAction action)
        {
            DatabaseManager.RefreshCacheFor(dbName);
            VM vm = action.Connection.Resolve(new XenRef<VM>(action.Result));
            if (vm == null)
                return false;

            return vm.power_state == vm_power_state.Halted;
        }

        private bool VmIsInstant(VM vm)
        {
            return vm.InstantTemplate;
        }
    }
}